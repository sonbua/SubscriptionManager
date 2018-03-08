using System.Reflection;
using Autofac;
using R2;
using R2.Aspect.Postprocessing;
using R2.Aspect.Preprocessing;
using R2.Aspect.Validation;
using R2.DependencyRegistration.Autofac;
using Module = Autofac.Module;

namespace SubscriptionManager.Subscriptions.DependencyRegistration.Autofac
{
    public class SubscriptionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var subscriptionsAssembly = Assembly.GetAssembly(typeof(Subscription));

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .AsClosedTypesOf(typeof(IPreprocessor<>))
                .As<IPreprocessor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .As<IValidator>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .BasedOn(typeof(IValidationRule<>))
                .AsSelf()
                .As<IValidationRule>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(IQueryHandler<,>),
                    serviceKey: "queryHandler")
                .As<IRequestHandler>()
                .As<IQueryHandler>()
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(QueryValidationDecorator<,>),
                    decoratedServiceType: typeof(IQueryHandler<,>),
                    fromKey: "queryHandler",
                    toKey: "queryValidation")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(QueryPostprocessingDecorator<,>),
                    decoratedServiceType: typeof(IQueryHandler<,>),
                    fromKey: "queryValidation",
                    toKey: "queryPostprocessing")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(QueryPreprocessingDecorator<,>),
                    decoratedServiceType: typeof(IQueryHandler<,>),
                    fromKey: "queryPostprocessing")
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(ICommandHandler<>),
                    serviceKey: "commandHandler")
                .As<IRequestHandler>()
                .As<ICommandHandler>()
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(CommandValidationDecorator<>),
                    decoratedServiceType: typeof(ICommandHandler<>),
                    fromKey: "commandHandler",
                    toKey: "commandValidation")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(CommandPreprocessingDecorator<>),
                    decoratedServiceType: typeof(ICommandHandler<>),
                    fromKey: "commandValidation")
                .InstancePerLifetimeScope();
        }
    }
}