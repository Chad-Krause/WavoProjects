import { buildEnvironment } from './environment.util';

const defaults = {
  production: true,
  dragUpdateDelayMS: 33,
  baseUrl: 'https://wavops.waverlyrobotics.org/',
  hubUrl: 'https://wavops.waverlyrobotics.org/WavOpsHub',
};

export const environment = buildEnvironment(defaults);
