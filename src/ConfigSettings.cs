﻿using System;

namespace PlanetInfoPlus
{
    /// <summary>
    /// Various settings loaded from the config file.
    /// </summary>
    internal class ConfigSettings
    {
        // Name of the relevant config section for this class.
        public const string CONFIG_NODE_NAME = "Settings";

        private const string INCLUDE_APPROXIMATE_LOCKED_ROTATION = "includeApproximateLockedRotation";
        private const string LOCKED_ROTATION_MINIMUM = "lockedRotationMinimum";
        private const string LOCKED_ROTATION_MAXIMUM = "lockedRotationMaximum";

        private static bool includeApproximateLockedRotation = false;
        private static double lockedRotationMinimum = 0.99;
        private static double lockedRotationMaximum = 1.01;

        public static bool IncludeApproximateLockedRotation
        {
            get { return includeApproximateLockedRotation; }
        }

        public static double LockedRotationMinimum
        {
            get { return lockedRotationMinimum; }
        }

        public static double LockedRotationMaximum
        {
            get { return lockedRotationMaximum; }
        }

        /// <summary>
        /// Load config values.
        /// </summary>
        /// <param name="node"></param>
        public static void LoadConfig(ConfigNode config)
        {
            for (int i = 0; i < config.values.Count; i++)
            {
                ConfigNode.Value value = config.values[i];
                try
                {
                    bool isValid = true;
                    switch (value.name)
                    {
                        case INCLUDE_APPROXIMATE_LOCKED_ROTATION:
                            includeApproximateLockedRotation = bool.Parse(value.value);
                            break;
                        case LOCKED_ROTATION_MINIMUM:
                            lockedRotationMinimum = double.Parse(value.value);
                            break;
                        case LOCKED_ROTATION_MAXIMUM:
                            lockedRotationMaximum = double.Parse(value.value);
                            break;
                        default:
                            Logging.Warn("Found unexpected config setting '" + value.name + ", ignoring");
                            isValid = false;
                            break;
                    }
                    if (isValid) Logging.Log(value.name + " = " + value.value);
                }
                catch (FormatException e)
                {
                    Logging.Error("Invalid value " + value.value + " found for " + value.name);
                }
            }
        }
    }
}