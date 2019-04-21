using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToastmastersRecord.Data.Migrations
{
    public partial class InitialDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMeetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaysOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    MeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvelopeEntityBase",
                columns: table => new
                {
                    StreamId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    Version = table.Column<short>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Event = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvelopeEntityBase", x => new { x.StreamId, x.UserId, x.Id });
                    table.UniqueConstraint("AK_EnvelopeEntityBase_Id_StreamId_UserId", x => new { x.Id, x.StreamId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "MemberHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    SpeechCountConfirmedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ConfirmedSpeechCount = table.Column<int>(nullable: false),
                    AggregateCalculationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CalculatedSpeechCount = table.Column<int>(nullable: false),
                    DateAsToastmaster = table.Column<DateTime>(type: "Date", nullable: false),
                    DateAsGeneralEvaluator = table.Column<DateTime>(type: "Date", nullable: false),
                    DateAsTableTopicsMaster = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastSpeech = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastEvaluation = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastMinorRole = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastMajorRole = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastFunctionaryRole = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastFacilitatorRole = table.Column<DateTime>(type: "Date", nullable: false),
                    DateOfLastRole = table.Column<DateTime>(type: "Date", nullable: false),
                    WillAttend = table.Column<bool>(nullable: false),
                    SpecialRequest = table.Column<string>(nullable: true),
                    EligibilityCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ToastmasterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Awards = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address5 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    PaidUntil = table.Column<DateTime>(type: "Date", nullable: false),
                    ClubMemberSince = table.Column<DateTime>(type: "Date", nullable: false),
                    OriginalJoinDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PaidStatus = table.Column<string>(nullable: true),
                    CurrentPosition = table.Column<string>(nullable: true),
                    FuturePosition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePlacements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MemberId = table.Column<Guid>(nullable: false),
                    RoleRequestId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    RoleTypeId = table.Column<int>(nullable: false),
                    MeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePlacements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    MessageId = table.Column<Guid>(nullable: false),
                    Brief = table.Column<string>(nullable: true),
                    MemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    MinimumSpeechCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Message = table.Column<string>(nullable: true),
                    MemberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRequestMeetings",
                columns: table => new
                {
                    RoleRequestId = table.Column<Guid>(nullable: false),
                    MeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRequestMeetings", x => new { x.RoleRequestId, x.MeetingId });
                    table.UniqueConstraint("AK_RoleRequestMeetings_MeetingId_RoleRequestId", x => new { x.MeetingId, x.RoleRequestId });
                    table.ForeignKey(
                        name: "FK_RoleRequestMeetings_RoleRequests_RoleRequestId",
                        column: x => x.RoleRequestId,
                        principalTable: "RoleRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MemberId",
                table: "Messages",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubMeetings");

            migrationBuilder.DropTable(
                name: "DaysOff");

            migrationBuilder.DropTable(
                name: "EnvelopeEntityBase");

            migrationBuilder.DropTable(
                name: "MemberHistories");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RolePlacements");

            migrationBuilder.DropTable(
                name: "RoleRequestMeetings");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "RoleRequests");
        }
    }
}
