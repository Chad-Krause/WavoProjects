const getEnvValue = (key: string): string | undefined => {
  if (typeof process === 'undefined' || typeof process.env === 'undefined') {
    return undefined;
  }

  console.log(`Environment variable ${key}: ${process.env[key]}`);

  return process.env[key];
};

const parseBoolean = (value: string | undefined, fallback: boolean): boolean => {
  if (value === undefined) {
    return fallback;
  }

  return value.toLowerCase() === 'true';
};

const parseNumber = (value: string | undefined, fallback: number): number => {
  if (value === undefined) {
    return fallback;
  }

  const parsed = Number(value);
  return Number.isNaN(parsed) ? fallback : parsed;
};

interface EnvironmentDefaults {
  production: boolean;
  dragUpdateDelayMS: number;
  baseUrl: string;
  hubUrl: string;
}

export const buildEnvironment = (defaults: EnvironmentDefaults) => ({
  production: parseBoolean(getEnvValue('NG_APP_PRODUCTION'), defaults.production),
  dragUpdateDelayMS: parseNumber(
    getEnvValue('NG_APP_DRAG_UPDATE_DELAY_MS'),
    defaults.dragUpdateDelayMS
  ),
  baseUrl: getEnvValue('NG_APP_BASE_URL') ?? defaults.baseUrl,
  hubUrl: getEnvValue('NG_APP_HUB_URL') ?? defaults.hubUrl,
});
