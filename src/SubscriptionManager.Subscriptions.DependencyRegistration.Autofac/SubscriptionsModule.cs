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
                    openGenericServiceType: typeof(IRequestHandler<,>),
                    serviceKey: "requestHandler")
                .As<IRequestHandler>()
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestValidationDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestHandler",
                    toKey: "requestValidation")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestPreprocessingDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestValidation",
                    toKey: "requestPreprocessing")
                .InstancePerLifetimeScope();
            builder
                .RegisterGenericDecorator(
                    decoratorType: typeof(RequestPostprocessingDecorator<,>),
                    decoratedServiceType: typeof(IRequestHandler<,>),
                    fromKey: "requestPreprocessing")
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(subscriptionsAssembly)
                .AsClosedTypesOf(
                    openGenericServiceType: typeof(ICommandHandler<>),
                    serviceKey: "commandHandler")
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