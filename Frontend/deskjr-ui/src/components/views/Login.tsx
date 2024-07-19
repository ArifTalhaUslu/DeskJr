import React, { useState } from 'react';
import Input from '../CommonComponents/Input';
import Button from '../CommonComponents/Button';
import Card from '../CommonComponents/Card';

const Login: any = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    // Handle login logic here
    console.log('Username:', username);
    console.log('Password:', password);
  };
  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-md-6">
          <Card title="Login">
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="username">Username</label>
                <Input
                  type="text"
                  className="form-control"
                  id="username"
                  value={username}
                  onChange={setUsername}
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
              <Button type='submit' text={'Login'}></Button>
              
            </form>
            
          </Card>
        </div>
      </div>
    </div>
  );
};

export default Login;