using System.Reflection;
using Autofac;
using R2;
using R2.Aspect.Postprocessing;
using R2.Aspect.Preprocessing;
using R2.Aspect.Validation;
using R2.DependencyRegistration.Autofac;
using SubscriptionManager.Subscriptions.AddSubscription;
using Module = Autofac.Module;

namespace SubscriptionManager.Subscriptions.DependencyRegistration.Autofac
{
    public class SubscriptionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadCommandsAndQueries(builder);

            LoadHandlers(builder);
        }

        private static void LoadCommandsAndQueries(ContainerBuilder builder)
        {
            var modelAssembly = Assembly.GetAssembly(typeof(Subscription));

            builder
                .RegisterAssemblyTypes(modelAssembly)
                .AssignableTo<ICommand>()
                .As<ICommand>()
                .InstancePerDependency();

            builder
                .RegisterAssemblyTypes(modelAssembly)
                .AssignableTo<IQuery>()
                .As<IQuery>()
                .InstancePerDependency();
        }

        private static void LoadHandlers(ContainerBuilder builder)
        {
            var handlerAssembly = Assembly.GetAssembly(typeof(AddSubscriptionCommandHandler));

            builder
                .RegisterAssemblyTypes(handlerAssembly)
                .AsClosedTypesOf(typeof(IPreprocessor<>))
                .As<IPreprocessor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(handlerAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .As<IValidator>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(handlerAssembly)
                .BasedOn(typeof(IValidationRule<>))
                .AsSelf()
                .As<IValidationRule>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(handlerAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(IQueryHandler<,>),
                    serviceKey: "queryHandler")
                .As<IRequestHandler>()
                .As<IQueryHandler>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(handlerAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(ICommandHandler<>),
                    serviceKey: "commandHandler")
                .As<IRequestHandler>()
                .As<ICommandHandler>()
                .InstancePerLifetimeScope();
        }
    }
}