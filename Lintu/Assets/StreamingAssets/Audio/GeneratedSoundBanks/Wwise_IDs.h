/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BARCOS_IDLE = 1876704714U;
        static const AkUniqueID COLISION_AGUA = 3661761514U;
        static const AkUniqueID COLISION_MADERA = 3999760164U;
        static const AkUniqueID CURSOR = 1330674255U;
        static const AkUniqueID CURSOR_SELECCION = 257651473U;
        static const AkUniqueID GANAR = 3575068712U;
        static const AkUniqueID INICIO_JUEGO = 1460932951U;
        static const AkUniqueID INICIO_MENU = 880580374U;
        static const AkUniqueID INICIO_START = 194776941U;
        static const AkUniqueID MENU_CREDITOS = 1063492266U;
        static const AkUniqueID ORB_PICKUP = 3042702347U;
        static const AkUniqueID PAJARO_ALETEA = 4246722315U;
        static const AkUniqueID PAJARO_VOZ = 2577500974U;
        static const AkUniqueID PAUSA_OFF = 2659475283U;
        static const AkUniqueID PAUSA_ON = 549173535U;
        static const AkUniqueID PERDER = 2433235219U;
        static const AkUniqueID RESTART = 1203400786U;
        static const AkUniqueID VOLVER_A_MENU = 3332192915U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMEPLAY
        {
            static const AkUniqueID GROUP = 89505537U;

            namespace STATE
            {
                static const AkUniqueID AMBIENTE_OFF = 271396146U;
                static const AkUniqueID AMBIENTE_ON = 3465855444U;
            } // namespace STATE
        } // namespace GAMEPLAY

        namespace MUSICA
        {
            static const AkUniqueID GROUP = 1730564739U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID OFF = 930712164U;
            } // namespace STATE
        } // namespace MUSICA

        namespace PAUSE
        {
            static const AkUniqueID GROUP = 3092587493U;

            namespace STATE
            {
                static const AkUniqueID OFF = 930712164U;
                static const AkUniqueID ON = 1651971902U;
            } // namespace STATE
        } // namespace PAUSE

        namespace SILENCE
        {
            static const AkUniqueID GROUP = 3041563226U;

            namespace STATE
            {
                static const AkUniqueID OFF = 930712164U;
                static const AkUniqueID ON = 1651971902U;
            } // namespace STATE
        } // namespace SILENCE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID ALTURA_PAJARO = 1768459312U;
        static const AkUniqueID ENERGIA_PAJARO = 201235242U;
        static const AkUniqueID LARGO_MAPA = 2301316308U;
        static const AkUniqueID VELOCIDAD_PAJARO = 2822622896U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENTE = 4095160060U;
        static const AkUniqueID CORAZON = 3566516997U;
        static const AkUniqueID FX = 1802970371U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSICA = 1730564739U;
        static const AkUniqueID SUB_MASTER = 2412407096U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID FX_ESPACIALES = 1792520288U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
