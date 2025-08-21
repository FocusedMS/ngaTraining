import React, {Component} from 'react';
import './userService.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as userServiceActions from "../../store/userService/actions";
export default class userService extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-user-service">Hello! component userService</div>;
    }
  }
// export default connect(
//     ({ userService }) => ({ ...userService }),
//     dispatch => bindActionCreators({ ...userServiceActions }, dispatch)
//   )( userService );