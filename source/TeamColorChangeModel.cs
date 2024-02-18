using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Core.Attributes;
using System.Drawing; 

namespace TeamColorChangePlugin;

[MinimumApiVersion(80)]
public class TeamColorChangePlugin : BasePlugin
{
    public override string ModuleName => "Team Color Change Model";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "ABKAM";
    public override string ModuleDescription => "Changes player model colors based on their team at the start of each round.";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventRoundStart>((@event, info) =>
        {
            foreach (var player in Utilities.FindAllEntitiesByDesignerName<CCSPlayerController>("cs_player_controller"))
            {
                if (player != null && player.IsValid && !player.IsBot)
                {
                    if (player.Team == CsTeam.Terrorist)
                    {
                        if (player.PlayerPawn != null && player.PlayerPawn.Value != null)
                        {
                            player.PlayerPawn.Value.Render = Color.FromArgb(255, 0, 0);
                        }
                    }
                    else if (player.Team == CsTeam.CounterTerrorist)
                    {
                        if (player.PlayerPawn != null && player.PlayerPawn.Value != null)
                        {
                            player.PlayerPawn.Value.Render = Color.FromArgb(0, 0, 255);
                        }
                    }
                }
            }
            return HookResult.Continue;
        }, HookMode.Post); 
    }
}