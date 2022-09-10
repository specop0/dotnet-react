export {};

declare global {
  interface Window {
    configuration: {
      backend: string;
    };
  }
}
