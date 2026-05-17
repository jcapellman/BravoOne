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
            // -- Real Cold War operations --------------------------------
            "URGENT FURY",          // Grenada 1983
            "JUST CAUSE",           // Panama 1989
            "ACID GAMBIT",          // Noriega capture, Panama 1989
            "BLUE SPOON",           // Original name for Just Cause
            "EAGLE CLAW",           // Iran hostage rescue attempt 1980
            "EARNEST WILL",         // Kuwait tanker escort 1987-88
            "NIMBLE ARCHER",        // Gulf of Iran oil platform strike 1987
            "PRAYING MANTIS",       // Largest US naval battle since WWII, 1988
            "PRIME CHANCE",         // Special ops vs Iran speedboats 1987
            "PAUL BUNYAN",          // DMZ tree-cutting incident, Korea 1976
            "SAND FLEA",            // Probing ops, Panama 1989
            "ACID GAMBIT",          // Delta Force, Panama 1989
            "IVORY COAST",          // Son Tay POW raid, Vietnam 1970
            "IVY BELLS",            // NSA submarine cable tap, Sea of Okhotsk
            "LOOKING GLASS",        // Airborne nuclear command post, Cold War
            "MOUNT HOPE III",       // Soviet helicopter capture, Chad 1988
            "STAUNCH VINE",         // Kuwait arms embargo enforcement 1987
            "SNOW BIRD",            // Arctic survival / Cold War contingency
            "CHROMITE",             // Inchon landing, Korea 1950
            "MARKET GARDEN",        // Arnhem airborne, 1944
            "OVERLORD",             // D-Day, Normandy 1944
            "ROLLING THUNDER",      // Vietnam bombing campaign 1965-68
            "VITTLES",              // Berlin Airlift 1948
            "DYNAMO",               // Dunkirk evacuation 1940
            "BARBAROSSA",           // German invasion of USSR — recovered intel
            "MAGIC CARPET",         // WWII troop repatriation
            "PAUL BUNYAN",          // Korea axe murder incident 1976
            // -- Gulf War era --------------------------------------------
            "DESERT SHIELD",
            "DESERT STORM",
            "DESERT SABRE",
            "DESERT STRIKE",
            "DESERT THUNDER",
            "DESERT FOX",
            "DESERT LION",
            "INSTANT THUNDER",
            "GRAND SLAM",
            // -- Action movie / fictional cold war flavor ----------------
            "RED DAWN",
            "RED STORM",
            "RED ZONE",
            "RED FLAG",
            "RED HEAT",
            "RED OCTOBER",
            "WRATH OF GOD",
            "KREMLIN GAMBIT",
            "BEAR TRAP",
            "WOLF PACK",
            "IRON CURTAIN",
            "IRON FIST",
            "IRON EAGLE",
            "IRON HAMMER",
            "IRON HAND",
            "BLACK HAWK",
            "BLACK OPS",
            "BLACK THUNDER",
            "BLACK SEPTEMBER",
            "BLACK HORNET",
            "BLACK WIDOW",
            "GHOST RECON",
            "GHOST RIDER",
            "GHOST PROTOCOL",
            "GHOST WIRE",
            "DARK STAR",
            "DARK HORSE",
            "DARK ANGEL",
            "DARK WINTER",
            "SILENT FURY",
            "SILENT EAGLE",
            "SILENT THUNDER",
            "SILENT PARTNER",
            "ARCTIC WOLF",
            "ARCTIC WIND",
            "ARCTIC STORM",
            "ARCTIC GHOST",
            "STEEL CURTAIN",
            "STEEL RAIN",
            "STEEL RESOLVE",
            "STEEL TALON",
            "BROKEN ARROW",
            "BROKEN LANCE",
            "BROKEN SWORD",
            "BURNING BRIDGE",
            "BURNING SANDS",
            "SCREAMING EAGLE",
            "SCREAMING HAWK",
            "MIDNIGHT EXPRESS",
            "MIDNIGHT HAMMER",
            "MIDNIGHT SUN",
            "MIDNIGHT WOLF",
            "COLD FIRE",
            "COLD FURY",
            "COLD FUSION",
            "COLD BLOOD",
            "CHECKMATE",
            "DEAD RECKONING",
            "LAST RESORT",
            "FINAL GAMBIT",
            "RAMROD",
            "CROSSBOW",
            "STILETTO",
            "JAVELIN",
            "NIGHTFALL",
            "NIGHTHAWK",
            "NIGHT STALKER",
            "NIGHT REAPER",
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
                    "Intel has gone dark in the region and Command is flying blind. Your team goes in silent — eyes on the installation, full thermal sweep, and out before the patrol rotation at 0400. No contact, no traces, no exceptions.",
                    "Satellite coverage over the valley has been jammed for seventy-two hours and we don't know why. Command needs boots on the ground to confirm whether those convoy signatures are armor or decoys. Get in, verify, and pull back before they know you were ever there.",
                    "A forward listening post went offline six hours ago and the operator missed his check-in. Scout the perimeter, assess the threat level, and determine whether the post can be reactivated or needs to be sanitized. Do not engage unless your exit is compromised.",
                    "The facility changed its guard schedule three days ago — same week a known GRU courier passed through the city. Command wants a full layout of the outer defenses before the next phase can be authorized. Your team has one window and it closes at dawn.",
                    "Someone tipped them off and now they're moving assets we've been tracking for months. You have forty-eight hours to reacquire the target's position before the trail goes cold. Recon only — if your team fires a single round, the whole operation burns."
                }
            },
            {
                ContractType.INFILTRATION,
                new[]
                {
                    "The research compound is running a four-layer security rotation and every approach is covered by overlapping fields of fire. Your team needs to crack the outer perimeter, reach the server room on sublevel three, and destroy the drives before the morning shift arrives. Every specialist on this one earns their pay tonight.",
                    "A mole inside the network has been feeding them our entire operational playbook for the last eight months. Command has identified the communications hub responsible for the leak — it needs to come down permanently and it cannot look deliberate. Get inside, plant the device, and be on the extraction bird before it blows.",
                    "They moved the defector to a black site forty-eight hours after we burned his contact. The address just came through back-channels and it won't be valid for long. Your team hits the compound, pulls the target, and vanishes — Command is watching but officially this never happened.",
                    "The biological research program they denied for three years is fully operational and forty feet underground. Infiltrate the facility, copy the schematics from the secure terminal, and destroy the primary culture lab on your way out. If any of that material leaves the site, the client's problems become everyone's problems.",
                    "Deep cover is no longer viable — their counterintelligence swept the network and two of our people are already in custody. The compound has to be hit tonight before the interrogations produce anything actionable. Go in hard, retrieve the cipher keys from the third-floor office, and leave nothing they can reconstruct."
                }
            },
            {
                ContractType.RESCUE,
                new[]
                {
                    "A logistics contractor was grabbed at a border checkpoint forty hours ago and local authorities have been paid to look the other way. Command has a probable location — a fortified warehouse on the edge of the industrial district — but the window to move is closing fast. Get him out before they decide he's worth more as a message than a ransom.",
                    "Three humanitarian aid workers were taken from their convoy two days ago and the organization they work for has gone quiet under pressure. Intelligence suggests they're being held in a compound twelve kilometers north of the last known contact point. Your team has the location, a narrow extraction window, and one chance to do this clean.",
                    "An undercover operative was burned by a source inside our own network and they moved her within six hours of the exposure. She's trained to hold out but nobody holds out forever — the clock started the moment they put the hood over her head. Extract before the interrogation team arrives from the capital.",
                    "The transport helicopter went down in hostile territory during a routine asset transfer and the crew is confirmed alive on the emergency beacon. Local forces will reach the crash site within eight hours and the crew cannot be taken — they know too much. Your team goes in, pulls the survivors, and we burn the wreck on the way out.",
                    "The target is a seventeen-year-old taken from a school convoy three days ago — her father is set to testify before an international tribunal next week and the timing is not a coincidence. The ransom demand is a stall while they arrange a more permanent solution. Move now, move fast, and do not negotiate."
                }
            },
            {
                ContractType.DEMOLITION,
                new[]
                {
                    "The bridge spanning the northern ravine is the only viable route for their armored column and the offensive kicks off in thirty-six hours. Your team plants the charges at the three load-bearing pylons, confirms placement with the laser designator, and clears the area before the timer runs. If that column reaches the valley floor, a lot of people die.",
                    "They finished construction on the radar array ahead of schedule and it goes live in seventy-two hours — after that, our aircraft lose every advantage they have in that corridor. Surgical placement only: the array, the backup generator, and the hardened relay station two hundred meters east. Make it look like a power-surge failure, not a strike.",
                    "A weapons cache was uncovered beneath a civilian port facility and the local government wants it handled quietly before the press finds out. Controlled demolition in a confined space with civilian infrastructure overhead — your demo specialist earns every dollar on this one. No blast signature, no casualties, no headlines.",
                    "The factory has been producing materiel in direct violation of three international agreements and the client wants it gone before inspectors arrive next month. Level the production wing, destroy the storage tanks on the south side, and leave nothing the engineers can rebuild from. The cover story is already written — your job is to make it credible.",
                    "The pipeline feeds every forward base in their eastern theater and Command wants it cut at the junction point forty kilometers inside hostile territory. Shaped charges at two points will cause a failure that looks like metal fatigue — your team has six hours from insertion to detonation before the maintenance crew runs its scheduled check."
                }
            },
            {
                ContractType.EXTRACTION,
                new[]
                {
                    "A deep-cover asset embedded in the target organization sent a single-word abort signal eighteen hours ago and then went completely dark. His cover is blown and he is almost certainly being held at the organization's primary compound while they decide what to do with him. Get to him before they make that decision.",
                    "A nuclear physicist with full knowledge of the program's current state attempted to defect through a third-party intermediary who turned out to be compromised. He is now trapped in a safe house behind their lines with no way out and a shrinking window before they find him. Your team is the only extraction option Command has left.",
                    "The diplomat's armored convoy was ambushed forty kilometers from the border and only one vehicle made it off the road intact. One survivor confirmed, location transmitted, hostiles still in the area and regrouping. Your team extracts the survivor and retrieves the diplomatic pouch — both objectives are non-negotiable.",
                    "The informant spent fourteen months building a case against the network and the evidence only exists in his head and on a single encrypted drive. He needs to be standing in a secure facility before his handler's deadline or the entire case collapses and everyone who helped him is exposed. Time from now to the border crossing: eleven hours.",
                    "The safe house was compromised four hours ago and the asset inside managed to send one burst transmission before going silent. Two other extraction teams were turned back by roadblocks that appeared within the hour — someone is feeding the opposition our routes in real time. Your team goes in off-grid, pulls the asset, and trusts nobody until they're across the border."
                }
            },
            {
                ContractType.SABOTAGE,
                new[]
                {
                    "Their entire eastern logistics network runs through a single hardened relay station and without it their supply chain collapses within seventy-two hours. The station is defended but not fortified — your team disables the primary and backup systems and is out before the technicians realize the redundancy has failed. No explosions, no bodies, no evidence.",
                    "The prototype weapons system is being transported by road convoy to a research facility where it will be reverse-engineered within weeks. Your team intercepts the convoy at the mountain pass, destroys the prototype, and is gone before the escort vehicles can radio for support. The client needs this to look like an accident.",
                    "Their communications infrastructure runs on a proprietary encryption system that Command has been trying to crack for two years. Your team plants a hardware exploit at the central node that will corrupt the network from the inside without triggering their intrusion detection. They cannot know they've been touched until it's too late to matter.",
                    "The forward operating base's entire fuel supply is stored in a hardened depot six kilometers from the main gate — without it, their armored assets are stationary within forty-eight hours. Your team infiltrates the depot perimeter, compromises the storage containment, and lets the resulting failure do the rest. Clean, quiet, deniable.",
                    "A device was planted by a third party inside a facility used by two opposing factions and neither side knows it's there. Your team locates the device, renders it inert, and extracts without either faction learning the other didn't plant it. If either side finds out what was almost done in their territory, the fragile ceasefire ends tonight."
                }
            },
            {
                ContractType.ASSASSINATION,
                new[]
                {
                    "The general has been selling the names of embedded assets to the highest bidder for three years and two of the people he burned are already dead. The client wants a permanent solution before the next auction — clean, professional, and completely untraceable back to anyone currently breathing. One shot from outside the perimeter. Your team is gone before the body is found.",
                    "He brokered the arms deal that resulted in twelve of our allies being ambushed on a road that was supposed to be secure. The evidence chain ends with him and the client has exhausted every legal option available. Your sniper has one confirmed window during the target's morning routine — after that, the security rotation changes and the opportunity disappears for months.",
                    "The arms dealer has a meet scheduled in a warehouse at the edge of the industrial port district and for the next four hours his entire network will be in one room. The client's instruction is simple: he does not leave that building. Your team has full latitude on method as long as the result is unambiguous and the location doesn't burn.",
                    "The target holds full diplomatic accreditation which makes every conventional approach legally impossible and every failure politically catastrophic. Your team needs to reach him during the forty-minute window when his security detail is reduced for the private dinner — make it look natural, make it clean, and be three countries away before the morning briefings.",
                    "Two years of surveillance, four burned approaches, and one confirmed window that opens for eleven minutes during the target's transfer between secure locations. The client has authorized maximum budget and has made clear there will not be a fifth attempt. Do not miss."
                }
            },
            {
                ContractType.ASSET_PROTECTION,
                new[]
                {
                    "A high-value witness is scheduled to testify before an international tribunal in seventy-two hours and three credible threats against his life have been received in the last week alone. The police detail assigned to him is compromised — one officer confirmed, two suspected. Your team replaces the detail entirely and gets him to the courthouse alive.",
                    "The data courier is carrying the only existing copy of an encryption key that controls access to a network worth billions and several people have already died trying to take it. The courier's route was leaked six hours ago and the client doesn't know how many teams are already in position. Your team is the last layer between the courier and everyone who wants what he's carrying.",
                    "There have been three assassination attempts against the principal in fourteen days — different methods, different teams, same result each time. The client needs a protection detail that thinks like the people trying to kill her, not like bodyguards. Your team assumes the contract immediately and treats every unknown as hostile until proven otherwise.",
                    "The convoy route was leaked to at least one hostile party before the ink was dry on the transport order. The client still needs the package moved and the schedule cannot change — your team takes the route knowing something is waiting on it and deals with whatever that is. The package arrives intact. That is the only acceptable outcome.",
                    "Their last security detail of four experienced contractors disappeared without a distress call, a shot fired, or a single piece of recoverable evidence. The client is paying double the standard rate and has not asked why the previous team is gone. Your team takes the contract with full knowledge that someone out there is very good at what they do."
                }
            }
        };
    }
}