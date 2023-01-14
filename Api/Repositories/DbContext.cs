using Api.Mapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{

    public class DbContext : IDbContext
    {
        private IMongoDatabase Database { get; set; }
        private List<Func<Task>> _commands;
        private static object _lock = new object();
        public DbContext(string connectionString, string dbName)
        {

            lock (_lock)
            {
                // Log.Information($"DbContext Initialized: {connectionString} : {dbName}");
                // Set Guid to CSharp style (with dash -)
                BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

                // Every command will be stored and it will be processed at SaveChanges
                _commands = new List<Func<Task>>();
                DefineClassMaps();
                RegisterConventions();

                // Configure mongo (You can inject the config, just to simplify)
                var mongoClient = new MongoClient(connectionString);

                Database = mongoClient.GetDatabase(dbName);
            }

        }

        private static void DefineClassMaps()
        {
            AbstractDbEntityMapper.Map();
            ReactionMapper.Map();
            
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public async Task<int> SaveChanges()
        {
            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Reset()
        {
            this._commands = new List<Func<Task>>();

            this.Dispose();
        }
    }
}
