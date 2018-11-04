using DgraphDotNet;
using DGraphExperiments.Serializer;

namespace DGraphExperiments.DGraph.Client
{
    public interface IDGraphClientSettings
    {
        string ConnectionString { get; }

        string Schema { get; }
    }

    public class DGraphClient
    {
        private readonly IDGraphClientSettings settings;
        private readonly IJsonSerializer serializer;

        public DGraphClient(IDGraphClientSettings settings)
            : this(settings, JsonSerializer.Default)
        {
        }

        public DGraphClient(IDGraphClientSettings settings, IJsonSerializer serializer)
        {
            this.settings = settings;
            this.serializer = serializer;
        }

        public void CreateSchema()
        {
            // Create a new Dgraph Client:
            using (var client = Clients.NewDgraphClient())
            {
                // Connects to the DGraph Instance:
                client.Connect(settings.ConnectionString);

                // Create the Schema:
                client.AlterSchema(settings.Schema);
            }
        }
        
        public void Mutate<TSourceType>(TSourceType source)
        {
            // Create a new Dgrapg Client:
            using (var client = Clients.NewDgraphClient())
            {
                // Connects to the DGraph Instance:
                client.Connect(settings.ConnectionString);

                using (var transaction = client.NewTransaction())
                {
                    // Serializes the Object(s) into JSON:
                    var json = serializer.SerializeObject(source);

                    // Mutate the Data:
                    transaction.Mutate(json);

                    // And finally try to commit the Transaction:
                    transaction.Commit();
                }
            }
        }
    }
}
