import LoginRequest from "../../commands/loginCommand/LoginRequest";
import LoginResponse from "../../commands/loginCommand/LoginResponse";

export default interface ILoginCommand {
    Handler: (request: LoginRequest) => Promise<LoginResponse>;
}
