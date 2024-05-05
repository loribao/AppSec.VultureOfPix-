import { inject, injectable } from "tsyringe";
import type ILoginRepository from "../../interfaces/IRepositories/ILoginRepository";
import LoginRequest from "./LoginRequest";
import LoginResponse from "./LoginResponse";
import ILoginCommand from "../../interfaces/ICommands/ILoginCommand";

@injectable()
class LoginCommand implements ILoginCommand{
    private loginRepository: ILoginRepository;
    constructor(@inject("ILoginRepository") loginRepository: ILoginRepository) {
        this.loginRepository = loginRepository;
    }
    public async  Handler(request: LoginRequest): Promise<LoginResponse> {
        //call to API
        let token = await this.loginRepository.login(request.username, request.password);
        //save token
        if (token.length > 0)
            return new LoginResponse("success");
        else
            throw new Error("Error on authenticate");
        //

    }
}

export default LoginCommand;
