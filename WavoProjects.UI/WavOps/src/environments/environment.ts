import { buildEnvironment } from './environment.util';

const defaults = {
  production: true,
  dragUpdateDelayMS: 33,
  baseUrl: 'https://localhost:44331/',
  hubUrl: 'https://localhost:44331/WavOpsHub',
};

export const environment = buildEnvironment(defaults);
