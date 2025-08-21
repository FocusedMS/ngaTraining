import React, {Component} from 'react';
import './demo.scss'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as demoActions from "../../store/demo/actions";
export default class demo extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-demo">Hello! component demo</div>;
    }
  }
// export default connect(
//     ({ demo }) => ({ ...demo }),
//     dispatch => bindActionCreators({ ...demoActions }, dispatch)
//   )( demo );