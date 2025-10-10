/*******************************************************************************
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
*******************************************************************************/
#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
namespace Wwise.AkCommandBuffer
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Critical
    }
    
    public interface ICommandBufferLogger
    {
        public  void Log(LogLevel logLevel, string message);
    }

    public static class ICommanddBufferLoggerExtensions
    {
        public static void LogDebug(this ICommandBufferLogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }
        
        public static void LogInfo(this ICommandBufferLogger logger, string message)
        {
            logger.Log(LogLevel.Info, message);
        }
        
        public static void LogWarning(this ICommandBufferLogger logger, string message)
        {
            logger.Log(LogLevel.Warn, message);
        }
        
        public static void LogError(this ICommandBufferLogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }
        
        public static void LogCritical(this ICommandBufferLogger logger, string message)
        {
            logger.Log(LogLevel.Critical, message);
        }
    }
}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.