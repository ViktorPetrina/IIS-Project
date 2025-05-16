using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhoneSpecsApi.Models
{
    public class Specification
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("phone_details_id")]
        [ForeignKey("PhoneDetails")]
        public long phoneDetailsId { get; set; }
        public PhoneDetails phoneDetails { get; set; }

        [Column("gsm_launch_details_id")]
        [ForeignKey("GsmLaunchDetails")]
        public long gsmLaunchDetailsId { get; set; }
        public GsmLaunchDetails gsmLaunchDetails { get; set; }

        [Column("gsm_body_details_id")]
        [ForeignKey("GsmBodyDetails")]
        public long gsmBodyDetailsId { get; set; }
        public GsmBodyDetails gsmBodyDetails { get; set; }

        [Column("gsm_display_details_id")]
        [ForeignKey("GsmDisplayDetails")]
        public long gsmDisplayDetailsId { get; set; }
        public GsmDisplayDetails gsmDisplayDetails { get; set; }

        [Column("gsm_memory_details_id")]
        [ForeignKey("GsmMemoryDetails")]
        public long gsmMemoryDetailsId { get; set; }
        public GsmMemoryDetails gsmMemoryDetails { get; set; }

        [Column("gsm_sound_details_id")]
        [ForeignKey("GsmSoundDetails")]
        public long gsmSoundDetailsId { get; set; }
        public GsmSoundDetails gsmSoundDetails { get; set; }

        [Column("gsm_battery_details_id")]
        [ForeignKey("GsmBatteryDetails")]
        public long gsmBatteryDetailsId { get; set; }
        public GsmBatteryDetails gsmBatteryDetails { get; set; }
    }

    public class GsmBatteryDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("battery_charging")]
        public string batteryCharging { get; set; }

        [Column("battery_type")]
        public string batteryType { get; set; }
    }

    public class GsmBodyDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("body_dimensions")]
        public string bodyDimensions { get; set; }

        [Column("body_weight")]
        public string bodyWeight { get; set; }

        [Column("body_sim")]
        public string bodySim { get; set; }

        [Column("body_build")]
        public string bodyBuild { get; set; }

        [Column("body_other1")]
        public string bodyOther1 { get; set; }

        [Column("body_other2")]
        public string bodyOther2 { get; set; }

        [Column("body_other3")]
        public string bodyOther3 { get; set; }
    }

    public class GsmDisplayDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("display_type")]
        public string displayType { get; set; }

        [Column("display_size")]
        public string displaySize { get; set; }

        [Column("display_resolution")]
        public string displayResolution { get; set; }

        [Column("display_protection")]
        public string displayProtection { get; set; }

        [Column("display_other1")]
        public string displayOther1 { get; set; }
    }

    public class GsmLaunchDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("launch_announced")]
        public string launchAnnounced { get; set; }

        [Column("launch_status")]
        public string launchStatus { get; set; }
    }

    public class GsmMemoryDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("memory_card_slot")]
        public string memoryCardSlot { get; set; }

        [Column("memory_internal")]
        public string memoryInternal { get; set; }

        [Column("memory_other1")]
        public string memoryOther1 { get; set; }
    }

    public class GsmSoundDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("sound_35mm_jack")]
        public string sound35MmJack { get; set; }

        [Column("sound_loudspeaker")]
        public string soundLoudspeaker { get; set; }

        [Column("sound_other1")]
        public string soundOther1 { get; set; }

        [Column("sound_other2")]
        public string soundOther2 { get; set; }
    }

    public class PhoneDetails
    {
        [Column("custom_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long customId { get; set; }

        [Column("year_value")]
        public string yearValue { get; set; }

        [Column("brand_value")]
        public string brandValue { get; set; }

        [Column("model_value")]
        public string modelValue { get; set; }
    }
}
