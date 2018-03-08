using System.Runtime.Serialization;

namespace KillBill.Client.Net.Model
{
    public enum EntitlementState
    {
        /* The entitlement was created in the future */
        [EnumMember(Value = "PENDING")]
        Pending,

        /* The entitlement was created in that initial state */
        [EnumMember(Value = "ACTIVE")]
        Active,

        /* The system blocked the entitlement */
        [EnumMember(Value = "BLOCKED")]
        Blocked,

        /* The user cancelled the entitlement */
        [EnumMember(Value = "CANCELLED")]
        Cancelled
    }
}