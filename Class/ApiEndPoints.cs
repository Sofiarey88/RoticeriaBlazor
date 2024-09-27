namespace RoticeriaBlazor.Class
{
    public static class ApiEndpoints
    {
        public static string Cliente { get; set; } = "clientes";

        public static string Pedido { get; set; } = "api/pedidos";
        public static string Producto { get; set; } = "api/productos";
        public static string Proveedor { get; set; } = "api/proveedores";

        public static string GetEndpoint(string name)
        {
            return name switch
            {
                nameof(Cliente) => Cliente,
                nameof(Pedido) => Pedido,
                nameof(Producto) => Producto,
                nameof(Proveedor) => Proveedor,
                _ => throw new ArgumentException($"Endpoint '{name}' no está definido.")
            };
        }
    }
}
