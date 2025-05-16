using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MobilePhoneSpecsApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GsmBatteryDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    battery_charging = table.Column<string>(type: "text", nullable: false),
                    battery_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmBatteryDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "GsmBodyDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    body_dimensions = table.Column<string>(type: "text", nullable: false),
                    body_weight = table.Column<string>(type: "text", nullable: false),
                    body_sim = table.Column<string>(type: "text", nullable: false),
                    body_build = table.Column<string>(type: "text", nullable: false),
                    body_other1 = table.Column<string>(type: "text", nullable: false),
                    body_other2 = table.Column<string>(type: "text", nullable: false),
                    body_other3 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmBodyDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "GsmDisplayDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_type = table.Column<string>(type: "text", nullable: false),
                    display_size = table.Column<string>(type: "text", nullable: false),
                    display_resolution = table.Column<string>(type: "text", nullable: false),
                    display_protection = table.Column<string>(type: "text", nullable: false),
                    display_other1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmDisplayDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "GsmLaunchDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    launch_announced = table.Column<string>(type: "text", nullable: false),
                    launch_status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmLaunchDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "GsmMemoryDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    memory_card_slot = table.Column<string>(type: "text", nullable: false),
                    memory_internal = table.Column<string>(type: "text", nullable: false),
                    memory_other1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmMemoryDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "GsmSoundDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sound_35mm_jack = table.Column<string>(type: "text", nullable: false),
                    sound_loudspeaker = table.Column<string>(type: "text", nullable: false),
                    sound_other1 = table.Column<string>(type: "text", nullable: false),
                    sound_other2 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GsmSoundDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneDetails",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year_value = table.Column<string>(type: "text", nullable: false),
                    brand_value = table.Column<string>(type: "text", nullable: false),
                    model_value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneDetails", x => x.custom_id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    custom_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_launch_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_body_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_display_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_memory_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_sound_details_id = table.Column<long>(type: "bigint", nullable: false),
                    gsm_battery_details_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.custom_id);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmBatteryDetails_gsm_battery_details_id",
                        column: x => x.gsm_battery_details_id,
                        principalTable: "GsmBatteryDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmBodyDetails_gsm_body_details_id",
                        column: x => x.gsm_body_details_id,
                        principalTable: "GsmBodyDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmDisplayDetails_gsm_display_details_id",
                        column: x => x.gsm_display_details_id,
                        principalTable: "GsmDisplayDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmLaunchDetails_gsm_launch_details_id",
                        column: x => x.gsm_launch_details_id,
                        principalTable: "GsmLaunchDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmMemoryDetails_gsm_memory_details_id",
                        column: x => x.gsm_memory_details_id,
                        principalTable: "GsmMemoryDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_GsmSoundDetails_gsm_sound_details_id",
                        column: x => x.gsm_sound_details_id,
                        principalTable: "GsmSoundDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Specifications_PhoneDetails_phone_details_id",
                        column: x => x.phone_details_id,
                        principalTable: "PhoneDetails",
                        principalColumn: "custom_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_battery_details_id",
                table: "Specifications",
                column: "gsm_battery_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_body_details_id",
                table: "Specifications",
                column: "gsm_body_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_display_details_id",
                table: "Specifications",
                column: "gsm_display_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_launch_details_id",
                table: "Specifications",
                column: "gsm_launch_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_memory_details_id",
                table: "Specifications",
                column: "gsm_memory_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_gsm_sound_details_id",
                table: "Specifications",
                column: "gsm_sound_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_phone_details_id",
                table: "Specifications",
                column: "phone_details_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "GsmBatteryDetails");

            migrationBuilder.DropTable(
                name: "GsmBodyDetails");

            migrationBuilder.DropTable(
                name: "GsmDisplayDetails");

            migrationBuilder.DropTable(
                name: "GsmLaunchDetails");

            migrationBuilder.DropTable(
                name: "GsmMemoryDetails");

            migrationBuilder.DropTable(
                name: "GsmSoundDetails");

            migrationBuilder.DropTable(
                name: "PhoneDetails");
        }
    }
}
