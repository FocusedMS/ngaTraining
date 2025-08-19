import React, {Component} from 'react';
import './contextEx1.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as contextEx1Actions from "../../store/contextEx1/actions";
export default class contextEx1 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-context-ex1">Hello! component contextEx1</div>;
    }
  }
// export default connect(
//     ({ contextEx1 }) => ({ ...contextEx1 }),
//     dispatch => bindActionCreators({ ...contextEx1Actions }, dispatch)
//   )( contextEx1 );