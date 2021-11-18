using System;
using System.Threading.Tasks;
using MassTransit;
using ParadiseCommerce.Contracts;

namespace ParadiseCommerce.Services.Provisioning.Consumers
{
    public class ProvisionProductConsumer : IConsumer<IProvisionProductCommand>
    {
        public Task Consume(ConsumeContext<IProvisionProductCommand> context)
        {
            Console.WriteLine("Consuming!!");
            Console.WriteLine("Customer: " + context.Message.CustomerId);

            if (context.Message.ProvisionVariables != null)
            {
                foreach (var dat in context.Message.ProvisionVariables)
                {
                    Console.WriteLine(dat.Key + ": " + dat.Value);
                }
            }

            return Task.CompletedTask;
        }
    }
}