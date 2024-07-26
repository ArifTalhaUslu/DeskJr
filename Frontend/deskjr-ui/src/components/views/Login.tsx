import React, { useState } from "react";
import Input from "../CommonComponents/Input";
import Button from "../CommonComponents/Button";
import Card from "../CommonComponents/Card";
import loginService from "../../services/LoginService";

const Login: React.FC = () => {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        await loginService.login({ email, password })
            .then((data: any) => {
                console.log(data);
            })
            .catch((err: any) => {
                console.log(err);
            });
    };

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <Card title="Login">
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <Input
                                    type="text"
                                    className="form-control"
                                    id="email"
                                    value={email}
                                    onChange={setEmail}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <Input
                                    type="password"
                                    className="form-control"
                                    id="password"
                                    value={password}
                                    onChange={setPassword}
                                    required
                                />
                            </div>
                            <Button type="submit" text="Login" />
                        </form>
                    </Card>
                </div>
            </div>
        </div>
    );
};

export default Login;
