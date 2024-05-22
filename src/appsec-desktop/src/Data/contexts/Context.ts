import IContext, { IApi, IStorage } from "../../Domain/interfaces/IContexts/IContext";
import TauriDriver from "../drivers/TauriDriver";
import WebDriver from "../drivers/WebDriver";

class Context implements IContext {
    private tauri: TauriDriver;
    private web: WebDriver;
    public Api: IApi;

    constructor() {
        this.tauri = new TauriDriver();
        this.web = new WebDriver();

        if (this.tauri.isDriver()) {
            this.Api = this.tauri.Api;
        }
        else if (this.web.isDriver()) {
            this.Api = this.web.Api;
        }
        else{
            throw new Error("No drivers found");
        }
    }
}

export default Context;
