import { inject, injectable } from "tsyringe";
import type ILoginRepository from "../../interfaces/IRepositories/ILoginRepository";
import LoginRequest from "./LoginRequest";
import LoginResponse from "./LoginResponse";
import ILoginCommand from "../../interfaces/ICommands/ILoginCommand";
import type IStoreRepository from "../../interfaces/IRepositories/IStoreRepository";

@injectable()
class LoginCommand implements ILoginCommand {

    constructor(@inject("ILoginRepository") private loginRepository: ILoginRepository) {
    }
    public async Handler(request: LoginRequest): Promise<LoginResponse> {
        //call to API
        let token = await this.loginRepository.login(request.username, request.password);
        //save token
        if (token.length > 0) {
            localStorage.setItem("token", token);
            return new LoginResponse("success");
        }
        else
            throw new Error("Error on authenticate");
    }
}

export default LoginCommand;
