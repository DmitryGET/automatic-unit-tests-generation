using Autofac;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.Chats;
using SampleProject.Domain.Messages;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.Users;
using SampleProject.Infrastructure.Domain;
using SampleProject.Infrastructure.Domain.Chats;
using SampleProject.Infrastructure.Domain.Messages;
using SampleProject.Infrastructure.Domain.Users;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ChatRepository>()
                .As<IChatRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MessageRepository>()
                .As<IMessageRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StronglyTypedIdValueConverterSelector>()
                .As<IValueConverterSelector>()
                .SingleInstance();
        }
    }
}