using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeBook.Configs
{
    public class NoteConfigs
    {
        public NoteConfigs()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings-sqlite.json")
                      .Build();
            var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
          
            MainDirectory = configuration.GetSection("ProjLoc").Value;
            MaterialLoc = configuration.GetSection("Material").Value;
            DataBaseLoc = configuration.GetSection("Database").Value;
        
          
        
          
        }
        public string DataBaseLoc { get; set; }
        public string MaterialLoc { get; set; }

        public string MainDirectory { get; set; }

    }
}
