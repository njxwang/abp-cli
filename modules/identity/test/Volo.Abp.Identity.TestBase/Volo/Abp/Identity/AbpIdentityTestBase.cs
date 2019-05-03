﻿using Volo.Abp.Modularity;

namespace Volo.Abp.Identity
{
    public abstract class AbpIdentityTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> 
        where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}
