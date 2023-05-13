
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product:BaseEntity
    {
        public string  Name { get; set; }
        public string  Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        [ForeignKey("ProductBrand")]
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set;}
    }
}