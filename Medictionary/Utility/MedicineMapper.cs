using Medictionary.Models;
using Medictionary.DTOS;
using System;
using System.Collections.Generic;

namespace Medictionary.Utility
{
    public class MedicineMapper
    {
        public static Medicine Map(MedicineDTO convertFrom)
        {
            var convertTo = new Medicine
            {
                ID = Guid.NewGuid().ToString(),
                IndustryID = convertFrom.IndustryID,
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

        public static List<Medicine> Map(List<MedicineDTO> convertFrom)
        {
            var convertTo = new List<Medicine>();
            foreach (var item in convertFrom)
            {
                convertTo.Add(MedicineMapper.Map(item));
            }
            return convertTo;
        }
    }
}