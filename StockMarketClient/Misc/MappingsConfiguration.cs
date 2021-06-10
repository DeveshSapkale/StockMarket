using AutoMapper;
using DataLayer.Entities;
using StockMarketClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketClient.Misc
{
   
    static class MappingsConfiguration
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Stock, StockViewModel>());
        public static Mapper mapper = new Mapper(config);
    }
}
