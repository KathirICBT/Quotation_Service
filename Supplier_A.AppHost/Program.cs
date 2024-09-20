var builder = DistributedApplication.CreateBuilder(args);

var productApi = builder.AddProject<Projects.Supplier_A_ProductService>("apiservice-product");
var OrderApi = builder.AddProject<Projects.Supplier_A_OrderService>("apiservice-order");

builder.AddProject<Projects.Supplier_A_web>("webfrontend")
    .WithReference(productApi)
    .WithReference(OrderApi);

builder.Build().Run();
