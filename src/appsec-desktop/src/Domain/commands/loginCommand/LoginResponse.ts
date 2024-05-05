class LoginResponse {
    public status: "success" | "error" = "error";

    constructor(status: "success" | "error") {
        this.status = status;
    }
}

export default LoginResponse;
