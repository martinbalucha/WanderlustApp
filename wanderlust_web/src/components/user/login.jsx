import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css'

/**
 * A component for a login
 */
export class Login extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
        <div className="App">
            <form method="POST">
                <h1>Welcome</h1>
                <div className="form-group">
                    <label for="usernameid">User name</label>
                    <div class="col-md-2">
                        <input type="text" placeholder="Username" id="usernameid" className="form-control" />
                    </div>                    
                </div>
                <div class="form-group">
                    <label for="usr">Name:</label>
                    <input type="text" class="form-control" id="usr" />
                </div>
            </form>
        </div>
        
        )
    }
}