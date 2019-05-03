﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Volo.Abp.BackgroundJobs.Hangfire
{
    public class HangfireJobExecutionAdapter<TArgs>
    {
        protected BackgroundJobOptions Options { get; }
        protected IServiceScopeFactory ServiceScopeFactory { get; }
        protected IBackgroundJobExecuter JobExecuter { get; }

        public HangfireJobExecutionAdapter(
            IOptions<BackgroundJobOptions> options, 
            IBackgroundJobExecuter jobExecuter, 
            IServiceScopeFactory serviceScopeFactory)
        {
            JobExecuter = jobExecuter;
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        public void Execute(TArgs args)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var jobType = Options.GetJob(typeof(TArgs)).JobType;
                var context = new JobExecutionContext(scope.ServiceProvider, jobType, args);
                JobExecuter.Execute(context);
            }
        }
    }
}
