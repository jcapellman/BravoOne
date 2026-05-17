using System.Collections.Generic;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Common
{
    public class Constants
    {
        public static readonly string[] FIRST_NAMES = new string[]
        {
            "JOHN", "JAMES", "JANE", "SANDRA", "KC", "AMY", "JARRED", "PATRICK",
            "MILES", "NOPHIL", "DREW", "ROB", "PERRY", "NICK", "AVIN", "DAK",
            "ALEJANDRO", "CLAY", "ANDREI", "WIL", "VICTOR", "ELENA", "COLE",
            "TASHA", "ROMAN", "DIESEL", "HECTOR", "IRIS", "LEON", "MAYA"
        };

        public static readonly string[] LAST_NAMES = new string[]
        {
            "ANDERSON", "RAMBO", "MCLANE", "PLISKEN", "SMITH", "WILLIAMS",
            "JONES", "MILLER", "DAVIS", "JOHNSON", "GARCIA", "MARTINEZ",
            "RODRIGUEZ", "WANG", "WILSON", "LEWIS", "HILL", "SCOTT",
            "MATRIX", "TRAUTMAN", "SHAFT", "RIGGS", "MURTAUGH", "BRADDOCK",
            "BURNETT", "CROCKETT", "TUBBS", "BOURNE", "ETHAN", "PREACHER"
        };

        public static readonly string[] MISSION_NAMES = new string[]
        {
            // Real Cold War / 80s–90s operations
            "OVERLORD", "ROLLING THUNDER", "RED DAWN", "VITTLES", "URGENT FURY",
            "DESERT STORM", "WRATH OF GOD", "BARBAROSSA", "MAGIC CARPET", "DYNAMO",
            "DESERT SHIELD", "DESERT STRIKE", "DESERT THUNDER", "DESERT FOX", "DESERT LION",
            "DAWN BLITZ", "DESTINED GLORY", "EAGLE CLAW", "EAGLE EYE", "DRAGOON RIDE",
            "FREEDOM FALCON", "FREEDOM EAGLE", "FREEDOM DEAL", "FREEDOM SENTINEL", "FREEDOM BANNER",
            "IVY BELLS", "IVORY COAST", "JOINT VENTURE", "LOOKING GLASS", "MOUNT HOPE",
            "NOBLE EAGLE", "BLACKJACK", "CHOPSTICK", "CORKSCREW", "DIADEM",
            "DOVETAIL", "FELIX", "MARKET GARDEN", "ICEBERG", "PATRIOT",
            "ROOSTER", "SAND FLEA", "SWORD", "TRIDENT", "TYPHOON", "VIPER",
            "CYCLONE", "JUST CAUSE", "BLUE SPOON", "ACID GAMBIT",
            "PRAYING MANTIS", "NIMBLE ARCHER", "EARNEST WILL", "PRIME CHANCE",
            "STAUNCH VINE", "SNOW BIRD", "PAUL BUNYAN", "CHROMITE",
            "CHROMITE II", "CHROMITE III", "GRAND SLAM", "GRAND EAGLE",
            "IRON FIST", "IRON HAND", "IRON EAGLE", "IRON HAMMER",
            "COBRA GOLD", "COBRA DANE", "COBRA DANE II",
            "SILENT FURY", "SILENT EAGLE", "SILENT THUNDER",
            "GHOST RECON", "GHOST RIDER", "GHOST PROTOCOL",
            "DARK STAR", "DARK HORSE", "DARK ANGEL",
            "BLACK HAWK", "BLACK OPS", "BLACK THUNDER", "BLACK SEPTEMBER",
            "WHITE STAR", "WHITE FLAG", "WHITE HEAT",
            "RED STORM", "RED ZONE", "RED FLAG",
            "ARCTIC WOLF", "ARCTIC WIND", "ARCTIC STORM",
            "SCREAMING EAGLE", "SCREAMING HAWK",
            "BURNING BRIDGE", "BURNING SANDS",
            "BROKEN LANCE", "BROKEN ARROW", "BROKEN SWORD",
            "STEEL CURTAIN", "STEEL RAIN", "STEEL RESOLVE",
            "MIDNIGHT EXPRESS", "MIDNIGHT HAMMER", "MIDNIGHT SUN",
            "COLD FIRE", "COLD FURY", "COLD FUSION",
            "KREMLIN GAMBIT", "WOLF PACK", "BEAR TRAP",
            "RAMROD", "CROSSBOW", "STILETTO", "JAVELIN",
            "NIGHTFALL", "NIGHTHAWK", "NIGHT STALKER",
            "CHECKMATE", "DEAD RECKONING", "LAST RESORT"
        };

        public static readonly string[] MISSION_PREFIX = new string[]
        {
            "OPERATION", "MISSION", "PROJECT", "TASK FORCE", "DIRECTIVE",
            "CODENAME", "CLASSIFIED", "PRIORITY"
        };

        // Per-type briefing lines. Randomly selected when a contract is generated.
        public static readonly Dictionary<ContractType, string[]> CONTRACT_BRIEFINGS =
            new Dictionary<ContractType, string[]>
        {
            {
                ContractType.RECON,
                new[]
                {
                    "Intel has gone dark in the region. Get eyes on the target and get out — no contact.",
                    "Command needs confirmation of enemy troop movements before the next move can be authorized.",
                    "A listening post has been compromised. Scout the perimeter and report back.",
                    "Satellite coverage is blind. Your team must physically verify the installation's status.",
                    "We have a window of 72 hours before the patrol rotation changes. Use it."
                }
            },
            {
                ContractType.INFILTRATION,
                new[]
                {
                    "The facility is heavily guarded. Get your team inside, retrieve the package, and vanish.",
                    "A mole has been feeding them our playbook. Infiltrate and destroy their communications hub.",
                    "The compound has four overlapping security grids. You'll need every specialist on this one.",
                    "Deep cover isn't an option anymore. Hit the compound hard and fast — leave nothing behind.",
                    "They moved the asset to a black site. Your clearance just got you the address. Your team gets the rest."
                }
            },
            {
                ContractType.RESCUE,
                new[]
                {
                    "A contractor was grabbed at the border. Local authorities are blind. Get him back.",
                    "Three aid workers are being held in a fortified warehouse. You have 48 hours.",
                    "An undercover operative was burned. Extract before the interrogation starts.",
                    "The helicopter went down in hostile territory. The crew is alive — for now.",
                    "A senator's daughter was taken. The ransom demand is a distraction. Move now."
                }
            },
            {
                ContractType.DEMOLITION,
                new[]
                {
                    "The bridge is the only route for their armored column. Take it down before dawn.",
                    "They've finished construction on the radar array. It goes live in 72 hours. It mustn't.",
                    "A weapons cache was discovered near the port. Controlled demolition — make it look like an accident.",
                    "The factory is producing something they shouldn't have. Level it. Leave no trace.",
                    "Command wants the pipeline cut. Surgical placement — maximum disruption, minimal exposure."
                }
            },
            {
                ContractType.EXTRACTION,
                new[]
                {
                    "A deep-cover asset has been compromised. Get to him before they do.",
                    "The scientist defected — problem is he's trapped behind their lines. Bring him in.",
                    "The diplomat's convoy was ambushed. One survivor. Your team is the only ride out.",
                    "The informant has names, dates, and locations. He needs to be standing in front of a judge by morning.",
                    "Word is the safehouse was compromised four hours ago. If your team moves now there's still a chance."
                }
            },
            {
                ContractType.SABOTAGE,
                new[]
                {
                    "Their supply chain depends on a single relay station. Disable it — quietly.",
                    "The prototype is in transit. Intercept and destroy before it reaches the research facility.",
                    "Corrupt their communications network. They cannot know it was us.",
                    "The fuel depot is the lifeblood of their forward operating base. Cut it off.",
                    "A sleeper agent planted the device. Your team needs to find it and make sure it doesn't work — for either side."
                }
            },
            {
                ContractType.ASSASSINATION,
                new[]
                {
                    "The general has been selling our people's names. The client wants a permanent solution.",
                    "He brokered the deal that got twelve of our allies killed. One shot, clean exit.",
                    "The arms dealer has a meet scheduled for Thursday. He doesn't leave that warehouse.",
                    "The target has diplomatic protection. Your team will need to be creative.",
                    "Two years of surveillance. One opportunity. Do not miss."
                }
            },
            {
                ContractType.ASSET_PROTECTION,
                new[]
                {
                    "A high-value witness is scheduled to testify in 72 hours. Keep him breathing.",
                    "The data courier carries the only copy of the encryption keys. Guard it with your lives.",
                    "Three assassination attempts in two weeks. The client needs a team that doesn't miss threats.",
                    "The convoy route has been leaked. Escort the package and deal with whatever's waiting.",
                    "Their last security detail disappeared. You're being paid double. There's a reason for that."
                }
            }
        };
    }
}
