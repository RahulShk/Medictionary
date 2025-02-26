using Medictionary.Models;
using Medictionary.DTOS;
using System;
using System.Collections.Generic;

namespace Medictionary.Utility
{
    public class MedicineMapper
    {
        public static Medicine Map(MedicineDTO convertFrom, Image image)
        {
            var convertTo = new Medicine
            {
                MedicineID = Guid.NewGuid().ToString(),
                IndustryID = convertFrom.IndustryID,
                MedicineImage = image,
                Name = convertFrom.Name,
                Composition = convertFrom.Composition,
                Manufacturer = convertFrom.Manufacturer,
                Batch = convertFrom.Batch,
                ManufacturingDate = convertFrom.ManufacturingDate,
                ExpiryDate = convertFrom.ExpiryDate,
                Price = convertFrom.Price,
                Stock = convertFrom.Stock
            };
            return convertTo;
        }

        public static List<Medicine> Map(List<MedicineDTO> convertFrom, Image image)
        {
            var convertTo = new List<Medicine>();
            foreach (var item in convertFrom)
            {
                convertTo.Add(MedicineMapper.Map(item, image));
            }
            return convertTo;
        }
    }
}