#if ANDROID
using Android.Util;
using Firebase.Crashlytics;
#endif

namespace PlanPlate.Utils
{
    public static class FirebaseCrashlyticsLogger 
    {
        public static void LogException(Exception ex)
        {
#if ANDROID21_0_OR_GREATER

            FirebaseCrashlytics.Instance.RecordException(Java.Lang.Throwable.FromException(ex));

#elif ANDROID

            try
            {
                throw ex;
            }
            catch (Exception e)
            {
                Log.Error("CustomException", e.Message);
            }

#endif
        }
    }
}
