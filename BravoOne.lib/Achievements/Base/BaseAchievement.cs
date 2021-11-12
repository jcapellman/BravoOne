using BravoOne.lib.Objects;

namespace BravoOne.lib.Achievements.Base
{
    public abstract class BaseAchievement
    {
        public abstract string Title { get; }

        public abstract string Description { get; }

        public abstract void VerifyAchievement(Game currentGame);
    }
}