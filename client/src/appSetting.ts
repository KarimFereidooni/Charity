export class AppSetting {
  public static get BackendUrl(): string {
    return typeof window === "undefined" || (window as any).webpackHotUpdate
      ? "https://localhost:5001"
      : document.location.origin;
  }
}
