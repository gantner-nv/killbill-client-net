using System;

namespace KillBill.Client.Net.Model
{
    public class BlockingState : KillBillObject
    {
        public Guid BlockedId { get; set; }

        public string StateName { get; set; }

        public string Service { get; set; }

        public bool BlockChange { get; set; }

        public bool BlockEntitlement { get; set; }

        public bool BlockBilling { get; set; }

        public DateTime EffectiveDate { get; set; }

        public BlockingStateType Type { get; set; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is BlockingState)) return false;

            BlockingState that = (BlockingState)o;
            if (BlockedId != null ? !BlockedId.Equals(that.BlockedId) : that.BlockedId != null) return false;
            if (StateName != null ? !StateName.Equals(that.StateName) : that.StateName != null) return false;
            if (Service != null ? !Service.Equals(that.Service) : that.Service != null) return false;
            if (!BlockChange.Equals(that.BlockChange)) return false;
            if (!BlockEntitlement.Equals(that.BlockEntitlement)) return false;
            if (!BlockBilling.Equals(that.BlockBilling)) return false;
            if (EffectiveDate != null ? EffectiveDate.CompareTo(that.EffectiveDate) != 0 : that.EffectiveDate != null) return false;
            return Type == that.Type;
        }

        public override int GetHashCode()
        {
            int result = BlockedId != null ? BlockedId.GetHashCode() : 0;
            result = (31 * result) + (StateName != null ? StateName.GetHashCode() : 0);
            result = (31 * result) + (Service != null ? Service.GetHashCode() : 0);
            result = (31 * result) + BlockChange.GetHashCode();
            result = (31 * result) + BlockEntitlement.GetHashCode();
            result = (31 * result) + BlockBilling.GetHashCode();
            result = (31 * result) + (EffectiveDate != null ? EffectiveDate.GetHashCode() : 0);
            result = (31 * result) + Type.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return "BlockingState{" +
                   "blockedId=" + BlockedId +
                   ", stateName='" + StateName + "\'" +
                   ", service='" + Service + "\'" +
                   ", blockChange=" + BlockChange +
                   ", blockEntitlement=" + BlockEntitlement +
                   ", blockBilling=" + BlockBilling +
                   ", effectiveDate=" + EffectiveDate +
                   ", type=" + Type +
                   "}";
        }
    }
}