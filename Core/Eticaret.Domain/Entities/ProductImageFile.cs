﻿namespace EticaretAPI.Domain.Entities;

public class ProductImageFile : File
	{
	public ICollection<Product> Products { get; set; }
	}