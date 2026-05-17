using BravoOne.lib.Achievements;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xunit;

namespace BravoOne.Tests
{
    public class AchievementTests
    {
        private static Game CreateGame() => new Game();

        // ── HireTeamMember ──────────────────────────────────────────────

        [Fact]
        public void HireTeamMember_NoMembers_ReturnsFalse()
        {
            var game = CreateGame();
            Assert.False(new HireTeamMember().VerifyAchievement(game));
        }

        [Fact]
        public void HireTeamMember_OneMember_ReturnsTrue()
        {
            var game = CreateGame();
            game.TeamMembers.Add(new TeamMember { Id = Guid.NewGuid(), Name = "Test", Status = TeamMemberStatus.OnTeam });
            Assert.True(new HireTeamMember().VerifyAchievement(game));
        }

        // ── HireTenTeamMembers ──────────────────────────────────────────

        [Fact]
        public void HireTenTeamMembers_TenMembers_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 10; i++)
                game.TeamMembers.Add(new TeamMember { Id = Guid.NewGuid(), Name = $"Member {i}", Status = TeamMemberStatus.OnTeam });
            Assert.False(new HireTenTeamMembers().VerifyAchievement(game));
        }

        [Fact]
        public void HireTenTeamMembers_ElevenMembers_ReturnsTrue()
        {
            var game = CreateGame();
            for (var i = 0; i < 11; i++)
                game.TeamMembers.Add(new TeamMember { Id = Guid.NewGuid(), Name = $"Member {i}", Status = TeamMemberStatus.OnTeam });
            Assert.True(new HireTenTeamMembers().VerifyAchievement(game));
        }

        // ── BuyEquipment ────────────────────────────────────────────────

        [Fact]
        public void BuyEquipment_NoEquipment_ReturnsFalse()
        {
            var game = CreateGame();
            Assert.False(new BuyEquipment().VerifyAchievement(game));
        }

        [Fact]
        public void BuyEquipment_OneEquipment_ReturnsTrue()
        {
            var game = CreateGame();
            game.TeamEquipment.Add(new Equipment());
            Assert.True(new BuyEquipment().VerifyAchievement(game));
        }

        // ── BuyTenEquipment ─────────────────────────────────────────────

        [Fact]
        public void BuyTenEquipment_NineEquipment_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 9; i++)
                game.TeamEquipment.Add(new Equipment());
            Assert.False(new BuyTenEquipment().VerifyAchievement(game));
        }

        [Fact]
        public void BuyTenEquipment_TenEquipment_ReturnsTrue()
        {
            var game = CreateGame();
            for (var i = 0; i < 10; i++)
                game.TeamEquipment.Add(new Equipment());
            Assert.True(new BuyTenEquipment().VerifyAchievement(game));
        }

        // ── CompleteContract ────────────────────────────────────────────

        [Fact]
        public void CompleteContract_NoContracts_ReturnsFalse()
        {
            var game = CreateGame();
            Assert.False(new CompleteContract().VerifyAchievement(game));
        }

        [Fact]
        public void CompleteContract_InProgressContract_ReturnsFalse()
        {
            var game = CreateGame();
            game.Contracts.Add(new Contract { Status = ContractStatus.InProgress });
            Assert.False(new CompleteContract().VerifyAchievement(game));
        }

        [Fact]
        public void CompleteContract_CompletedContract_ReturnsTrue()
        {
            var game = CreateGame();
            game.Contracts.Add(new Contract { Status = ContractStatus.Completed });
            Assert.True(new CompleteContract().VerifyAchievement(game));
        }

        // ── CompleteTenContracts ────────────────────────────────────────

        [Fact]
        public void CompleteTenContracts_TenCompleted_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 9; i++)
                game.RecordContractCompleted();
            Assert.False(new CompleteTenContracts().VerifyAchievement(game));
        }

        [Fact]
        public void CompleteTenContracts_ElevenCompleted_ReturnsTrue()
        {
            var game = CreateGame();
            for (var i = 0; i < 10; i++)
                game.RecordContractCompleted();
            Assert.True(new CompleteTenContracts().VerifyAchievement(game));
        }

        // ── SurvivedAYear ───────────────────────────────────────────────

        [Fact]
        public void SurvivedAYear_ElevenMonths_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 11; i++) game.EndTurn();
            Assert.False(new SurvivedAYear().VerifyAchievement(game));
        }

        [Fact]
        public void SurvivedAYear_TwelveMonths_ReturnsTrue()
        {
            var game = CreateGame();
            for (var i = 0; i < 12; i++) game.EndTurn();
            Assert.True(new SurvivedAYear().VerifyAchievement(game));
        }

        [Fact]
        public void SurvivedAYear_ThirteenMonths_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 13; i++) game.EndTurn();
            Assert.False(new SurvivedAYear().VerifyAchievement(game));
        }

        // ── SurvivedTenYears ────────────────────────────────────────────

        [Fact]
        public void SurvivedTenYears_119Months_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 119; i++) game.EndTurn();
            Assert.False(new SurvivedTenYears().VerifyAchievement(game));
        }

        [Fact]
        public void SurvivedTenYears_120Months_ReturnsTrue()
        {
            var game = CreateGame();
            for (var i = 0; i < 120; i++) game.EndTurn();
            Assert.True(new SurvivedTenYears().VerifyAchievement(game));
        }

        [Fact]
        public void SurvivedTenYears_121Months_ReturnsFalse()
        {
            var game = CreateGame();
            for (var i = 0; i < 121; i++) game.EndTurn();
            Assert.False(new SurvivedTenYears().VerifyAchievement(game));
        }
    }
}
