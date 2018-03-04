﻿using Autofac;
using R2.Net.Mail.SystemNetSmtp;
using Raven.Client.Document;

namespace SubscriptionManager.DependencyRegistration.Autofac
{
    public class SubscriptionManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SystemNetSmtpMailServiceSettings>()
                .As<ISystemNetSmtpMailServiceSettings>()
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(
                    new DocumentStore
                    {
                        Url = "http://localhost:8080",
                        DefaultDatabase = "Test"
                    }.Initialize())
                .SingleInstance();
        }
    }
}