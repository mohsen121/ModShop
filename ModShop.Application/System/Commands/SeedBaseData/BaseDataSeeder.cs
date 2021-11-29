using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using ModShop.Domain.Entities;

namespace ModShop.Application.System.Commands.SeedBaseData
{
    public class BaseDataSeeder
    {
        private IAppDb _appDb;

        public BaseDataSeeder(IAppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {

        }

        //private async Task SeedAppConfigWithConfigKey(AppConfigKey configKey, string value, ConfigValueType valueType, CancellationToken cancellationToken)
        //{
        //    var appConfig = new AppConfig
        //    {
        //        ConfigKey = configKey,
        //        Value = value,
        //        ValueType = valueType
        //    };
        //    _appDb.AppConfigs.Add(appConfig);
        //    await _appDb.SaveChangesAsync(cancellationToken);
        //}

        //private async Task SeedAppConfigs(CancellationToken cancellationToken)
        //{
        //    var list = new[]
        //    {
        //        new AppConfig
        //        {
        //            ConfigKey = AppConfigKey.NServiceBusConcurrency,
        //            ValueType = ConfigValueType.Int,
        //            Value = "8"
        //        },
        //        new AppConfig
        //        {
        //            ConfigKey = AppConfigKey.ZipCodeCountry,
        //            ValueType = ConfigValueType.String,
        //            Value = "US"
        //        },
        //        new AppConfig
        //        {
        //            ConfigKey = AppConfigKey.NServiceBusImmediateRetryCount,
        //            ValueType = ConfigValueType.Int,
        //            Value = "3"
        //        },
        //        new AppConfig
        //        {
        //            ConfigKey = AppConfigKey.NServiceBusDelayedRetryCount,
        //            ValueType = ConfigValueType.Int,
        //            Value = "3"
        //        },

        //    };
        //    _appDb.AppConfigs.AddRange(list);
        //    await _appDb.SaveChangesAsync(cancellationToken);
        //}




    }
}
