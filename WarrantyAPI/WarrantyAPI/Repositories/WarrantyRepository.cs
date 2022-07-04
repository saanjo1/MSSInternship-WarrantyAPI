using AutoMapper;
using Microsoft.Azure.Cosmos.Table;
using WarrantyAPI.Contracts;
using WarrantyAPI.Models;

namespace WarrantyAPI.Repositories
{
    public class WarrantyRepository : IRepository<Warranty>
    {
        private string _connectionString;
        private string _tableName;
        private CloudTableClient _tableClient;
        private CloudTable _table;
        private readonly IMapper _mapper;


        public WarrantyRepository(string connectionString, string tableNamePrefix, IMapper mapper)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException("connectionString", "connectionString is null");
            _tableName = tableNamePrefix ?? throw new ArgumentNullException("tablePrefix", "tablePrefix is null");
            CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse(connectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(tableNamePrefix);
            _table.CreateIfNotExists();
            _mapper = mapper;

        }

        public async Task<bool> Delete(string warrantyId, string assetId)
        {
            Console.WriteLine("Entering Delete task in repository.");

            WarrantyTableEntity warrantyEntity = new WarrantyTableEntity
            {
                PartitionKey = warrantyId,
                RowKey = assetId,
                ETag = "*"
            };
            Console.WriteLine("Executing TableOperation delete in repository.");

            TableOperation deleteOperation = TableOperation.Delete(warrantyEntity);
            var tableResult = _table.Execute(deleteOperation);
            Console.WriteLine("Finished delete in repository.");

            return tableResult.HttpStatusCode == 204 ? true : false;
        }

        public async Task<string> Create(Warranty warranty)
        {
            Console.WriteLine("Entering Create task in repository.");

            WarrantyTableEntity warrantyEntity = new WarrantyTableEntity(warranty.WarrantyID, warranty.AssetID);

            Console.WriteLine("Mapping with AutoMapper.");

            _mapper.Map(warranty, warrantyEntity);
            warrantyEntity.WarrantyID = Guid.NewGuid().ToString();

            Console.WriteLine("Executing InsertOrReplace TableOperation in repository.");

            TableOperation insertOperation = TableOperation.InsertOrReplace(warrantyEntity);
            var tableresult = _table.Execute(insertOperation);



            return await Task.FromResult(tableresult.HttpStatusCode == 204 ? "Warranty created successfully." : "An error occured while creating a warranty.");
        }

        public async Task<Warranty> Read(string assetId)
        {

            Console.WriteLine("Filtering for Read task in repository.");

            var filter = $"RowKey eq '{assetId}'";
            var query = new TableQuery<WarrantyTableEntity>().Where(filter);

            Console.WriteLine("Executing query for Read task in repository.");

            var lst = _table.ExecuteQuery(query);
            var result = lst.Select(x => _mapper.Map<Warranty>(x)).FirstOrDefault();

            return result;
        }

        public async Task<IEnumerable<Warranty>> Read()
        {
            Console.WriteLine("Getting all data - Read task in repository.");

            var query = new TableQuery<WarrantyTableEntity>();
            var lst = _table.ExecuteQuery(query);

            Console.WriteLine("Getting all data - Mapping data in repository.");

            var result = lst.Select(x => _mapper.Map<Warranty>(x));


            return result;
        }

        public async Task<bool> Update(Warranty warranty)
        {
            Console.WriteLine("Entering Update data task in repository.");

            WarrantyTableEntity warrantyEntity = new WarrantyTableEntity(warranty.WarrantyID, warranty.AssetID);

            Console.WriteLine("Mapping with AutoMapper.");

            _mapper.Map(warranty, warrantyEntity);

            TableOperation insertOperation = TableOperation.InsertOrMerge(warrantyEntity);

            var operationResult = _table.Execute(insertOperation);

            Console.WriteLine("Executing query for Update task in repository.");

            return operationResult.HttpStatusCode == 204 ? true : false;
        }
    }
}
