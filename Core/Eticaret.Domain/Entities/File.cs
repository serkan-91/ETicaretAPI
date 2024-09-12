namespace EticaretAPI.Domain.Entities;

public class File : BaseEntity
{
	[NotMapped]
	public override DateTime? UpdatedDate
	{
		get => base.UpdatedDate;
		set => base.UpdatedDate = value;
	}
	public string FileName { get; set; }
	public string Path { get; set; }
	public enum StorageType
		{
		Local,
		Azure,
		AWS
		}
	}
