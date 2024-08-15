namespace common.library.SeedWork
{
    public abstract class BaseEntity
    {
        public Guid ID { get; protected set; }
        public DateTimeOffset CreatedOn { get; protected set; }
        public string CreatedBy { get; protected set; }
        public string CreatedByType { get; protected set; }
        
        public DateTimeOffset ModifiedOn { get; protected set; }
        public string ModifiedBy { get; protected set; }
        public string ModifiedByType { get; protected set; }
        public bool IsActive { get; protected set; }

        protected BaseEntity() { }
        protected BaseEntity(string id, string userType)
        {
            DateTime tempDateTime = DateTime.SpecifyKind(DateTime.Now.AddHours(8), DateTimeKind.Unspecified);
            DateTimeOffset convertedDateTime = new DateTimeOffset(tempDateTime, TimeSpan.FromHours(8));

            CreatedOn = convertedDateTime;
            ModifiedOn = convertedDateTime;

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(nameof(id));
            else
            {
                CreatedBy = id;
                ModifiedBy = id;
            }

            if (string.IsNullOrEmpty(userType) || string.IsNullOrWhiteSpace(userType))
                throw new ArgumentException(nameof(userType));
            else
            {
                CreatedByType = userType;
                ModifiedByType = userType;
            }

            IsActive = true;
        }

        protected void SetModifiedBy(string id, string userType)
        {
            DateTime tempDateTime = DateTime.SpecifyKind(DateTime.Now.AddHours(8), DateTimeKind.Unspecified);
            DateTimeOffset convertedDateTime = new DateTimeOffset(tempDateTime, TimeSpan.FromHours(8));

            ModifiedOn = convertedDateTime;

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(nameof(id));
            else
            {
                ModifiedBy = id;
            }

            if (string.IsNullOrEmpty(userType) || string.IsNullOrWhiteSpace(userType))
                throw new ArgumentException(nameof(userType));
            else
            {
                ModifiedByType = userType;
            }
        }

        protected void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        protected void SetID(Guid id)
        {
            ID = id;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is BaseEntity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (ID == default || other.ID == default)
                return false;

            return ID == other.ID;
        }

#pragma warning disable S3875 // "operator==" should not be overloaded on reference types
        public static bool operator ==(BaseEntity left, BaseEntity right)
#pragma warning restore S3875 // "operator==" should not be overloaded on reference types
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}{ID}".GetHashCode();
        }
        protected void SetModifiedOn(DateTime modifiedOnTimestamp)
        {
            DateTimeOffset convertedDateTime = new DateTimeOffset(modifiedOnTimestamp, TimeSpan.FromHours(8));
            ModifiedOn = convertedDateTime;
        }
        protected void SetCreatedOn(DateTime createdOnTimeStamp)
        {
            DateTimeOffset convertedDateTime = new DateTimeOffset(createdOnTimeStamp, TimeSpan.FromHours(8));
            CreatedOn = convertedDateTime;
        }
    }
}
