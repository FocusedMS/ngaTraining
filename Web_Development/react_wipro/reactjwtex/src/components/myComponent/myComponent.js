import React, {Component} from 'react';
import './myComponent.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as myComponentActions from "../../store/myComponent/actions";
export default class myComponent extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-my-component">Hello! component myComponent</div>;
    }
  }
// export default connect(
//     ({ myComponent }) => ({ ...myComponent }),
//     dispatch => bindActionCreators({ ...myComponentActions }, dispatch)
//   )( myComponent );