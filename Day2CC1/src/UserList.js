import React, { Component } from 'react';

class UserList extends Component {
  constructor(props) {
    super(props);
    this.state = {
      message: 'User list loaded'
    };
  }

  componentDidMount() {
    console.log('UserList component mounted');
  }

  componentDidUpdate(prevProps) {
    if (prevProps.users.length !== this.props.users.length) {
      this.setState({ message: `User list updated. Total users: ${this.props.users.length}` });
    }
  }

  render() {
    const { users } = this.props;
    
    return (
      <div>
        <h2>{this.state.message}</h2>
        <ul>
          {users.map(user => (
            <li key={user.id}>
              {user.name} - {user.email}
            </li>
          ))}
        </ul>
      </div>
    );
  }
}

export default UserList;
