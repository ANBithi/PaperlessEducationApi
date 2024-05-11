using Api.Mapper;
using Api.Mapper.Results;
using Api.Mapper.UserSpecific;
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
                // Set Guid to CSharp style (with dash -)
                BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

                _commands = new List<Func<Task>>();
                DefineClassMaps();
                RegisterConventions();

                var mongoClient = new MongoClient(connectionString);

                Database = mongoClient.GetDatabase(dbName);
            }

        }

        private static void DefineClassMaps()
        {
            AbstractDbEntityMapper.Map();
            ReactionMapper.Map();
            CommentMapper.Map();
            PostMapper.Map();
            InteractionMapper.Map();
            AcitivityMapper.Map();
            CourseMapper.Map();
            ExamMapper.Map();
            ResultMapper.Map();
            PasswordMetadataMapper.Map();
            AnswerMapper.Map();
            QuestionMapper.Map();
            DepartmentMapper.Map();
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
