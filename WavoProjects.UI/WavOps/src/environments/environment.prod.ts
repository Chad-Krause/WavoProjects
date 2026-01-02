import { buildEnvironment } from './environment.util';

const defaults = {
  production: true,
  dragUpdateDelayMS: 33,
  baseUrl: 'https://apiv2.waverlyrobotics.org/',
  hubUrl: 'https://apiv2.waverlyrobotics.org/WavOpsHub',
};

export const environment = buildEnvironment(defaults);
