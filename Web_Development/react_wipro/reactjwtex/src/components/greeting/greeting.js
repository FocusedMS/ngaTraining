import React, {Component} from 'react';
import './greeting.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as greetingActions from "../../store/greeting/actions";
export default class greeting extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-greeting">Hello! component greeting</div>;
    }
  }
// export default connect(
//     ({ greeting }) => ({ ...greeting }),
//     dispatch => bindActionCreators({ ...greetingActions }, dispatch)
//   )( greeting );