import IContext from "../IContexts/IContext";

interface IDriver extends IContext{
    isDriver(): boolean;
    isDriverAsync(): Promise<boolean>;
}

export default IDriver;
